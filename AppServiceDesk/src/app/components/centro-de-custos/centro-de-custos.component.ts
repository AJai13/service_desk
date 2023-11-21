import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CentroDeCustosService } from 'src/app/centro-de-custos.service';
import { Categoria } from 'src/app/Categoria';
import { Usuario } from 'src/app/Usuario';
import { UsuariosService } from 'src/app/usuarios.service';
import { CentroDeCusto } from 'src/app/CentroDeCusto';
import { CategoriasService } from 'src/app/categorias.service';


@Component({
  selector: 'app-centro-de-custos',
  templateUrl: './centro-de-custos.component.html',
  styleUrls: ['./centro-de-custos.component.css']
})
export class CentroDeCustosComponent implements OnInit  {
  formulario: any;
  tituloFormulario: string = '';
  centroDeCustos: Array<CentroDeCusto> | undefined;
  usuarios: Array<Usuario> | undefined;
  categorias: Array <Categoria> | undefined;

  constructor(
    private centroDeCustosService: CentroDeCustosService,
    private usuariosService: UsuariosService,
    private categoriasService: CategoriasService,
  ) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Novo centro de custo';
    this.centroDeCustosService.listar().subscribe((centroDeCustos) => {
      this.centroDeCustos = centroDeCustos;
    });
    this.usuariosService.listar().subscribe((usuarios) => {
      this.usuarios = usuarios;
      if (this.usuarios && this.usuarios.length > 0) {
        this.formulario.get('usuarioId')?.setValue(this.usuarios[0].id);
      }
    });
    this.categoriasService.listar().subscribe((categorias) => {
      this.categorias = categorias;
      if (this.categorias && this.categorias.length > 0) {
        this.formulario.get('categoriaId')?.setValue(this.categorias[0].id);
      }
    });
    this.formulario = new FormGroup({
      nome: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const centroDeCusto: CentroDeCusto = this.formulario.value;
    delete centroDeCusto.id;
    this.centroDeCustosService.cadastrar(centroDeCusto).subscribe((result) => {
      alert('CentroDeCusto inserido com sucesso.');
    });
  }

  excluir(): void {
    const centroDeCusto: CentroDeCusto = this.formulario.value;
    this.centroDeCustosService.excluir(centroDeCusto.id).subscribe((result) => {
      alert('CentroDeCusto excluído com sucesso.');
    });
  }

  alterar(): void {
    const centroDeCusto: CentroDeCusto = this.formulario.value;
    var id = centroDeCusto.id;
    delete centroDeCusto.id;
    this.centroDeCustosService.alterar(id, centroDeCusto).subscribe((result) => {
      alert('CentroDeCusto alterado com sucesso.');
    });
  }
}
