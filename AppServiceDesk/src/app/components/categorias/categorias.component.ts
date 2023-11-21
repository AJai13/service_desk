import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CategoriasService } from 'src/app/categorias.service';
import { Categoria } from 'src/app/Categoria';

@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.component.html',
  styleUrls: ['./categorias.component.css'],
})
export class CategoriasComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  categorias: Array<Categoria> | undefined;

  constructor(private categoriasService: CategoriasService) { }
  ngOnInit(): void {
    this.tituloFormulario = 'Nova categoria';
    this.categoriasService.listar().subscribe(categorias => {
      this.categorias = categorias;
    });
    this.formulario = new FormGroup({
      nome: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const categoria: Categoria = this.formulario.value;
    delete categoria.id;
    this.categoriasService.setCategoria(categoria).subscribe((result) => {
      alert('Categoria inserida com sucesso.');
    });
  }
  excluir(): void {
    const categoria: Categoria = this.formulario.value;
    this.categoriasService.excluir(categoria.id).subscribe((result) => {
      alert('Categoria excluído com sucesso.');
    });
    console.log(categoria);
  }

  alterar(): void {
    const categoria: Categoria = this.formulario.value;
    var id = categoria.id;
    delete categoria.id;
    this.categoriasService.alterar(id, categoria).subscribe((result) => {
      alert('Categoria alterado com sucesso.');
    });
  }
}
