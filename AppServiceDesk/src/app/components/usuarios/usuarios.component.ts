import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UsuariosService } from 'src/app/usuarios.service';
import { Usuario } from 'src/app/Usuario';
import { CentroDeCusto } from 'src/app/CentroDeCusto';
import { CentroDeCustosService } from 'src/app/centro-de-custos.service';
import { Dispositivo } from 'src/app/Dispositivo';
import { DispositivosService } from 'src/app/dispositivos.service';


@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  dispositivos: Array<Dispositivo> | undefined;
  centroDeCustos: Array<CentroDeCusto> | undefined;
  usuarios: Array<Usuario> | undefined;

  constructor(
    private dispositivosService: DispositivosService,
    private usuariosService: UsuariosService,
    private centroDeCustosService: CentroDeCustosService,
  ) { }
  ngOnInit(): void {
    this.tituloFormulario = 'Novo usuário';
    this.usuariosService.listar().subscribe((usuarios) => {
      this.usuarios = usuarios;
    });
    this.dispositivosService.listar().subscribe((dispositivos) => {
      this.dispositivos = dispositivos;
      if (this.dispositivos && this.dispositivos.length > 0) {
        this.formulario.get('dispositivoId')?.setValue(this.dispositivos[0].id);
      }
    });
    this.centroDeCustosService.listar().subscribe((centroDeCustos) => {
      this.centroDeCustos = centroDeCustos;
      if (this.centroDeCustos && this.centroDeCustos.length > 0) {
        this.formulario.get('centroDeCustoId')?.setValue(this.centroDeCustos[0].id);
      }
    });
    this.formulario = new FormGroup({
      id: new FormControl(""),
      nome: new FormControl(""),
      email: new FormControl(""),
      centroDeCustoId: new FormControl(""),
      dispositivoId: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const usuario: Usuario = this.formulario.value;
    this.usuariosService.cadastrar(usuario).subscribe((result) => {
      alert('Usuário inserido com sucesso.');
    });
  }

  excluir(): void {
    const usuario: Usuario = this.formulario.value;
    this.usuariosService.excluir(usuario.id).subscribe((result) => {
      alert('Usuário excluído com sucesso.');
    });
  }

  alterar(): void {
    const usuario: Usuario = this.formulario.value;
    var id = usuario.id;
    delete usuario.id;
    this.usuariosService.alterar(id, usuario).subscribe((result) => {
      alert('Usuário alterado com sucesso.');
    });
  }
}
