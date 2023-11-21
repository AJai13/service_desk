import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { FiltrosService } from 'src/app/filtros.service';
import { Filtro } from 'src/app/Filtro';
@Component({
  selector: 'app-filtros',
  templateUrl: './filtros.component.html',
  styleUrls: ['./filtros.component.css']
})
export class FiltrosComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  filtros: Array<Filtro> | undefined;

  constructor(private filtrosService: FiltrosService) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Novo filtro';
    this.filtrosService.listar().subscribe(filtros => {
      this.filtros = filtros;
    });
    this.formulario = new FormGroup({
      nome: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const filtro: Filtro = this.formulario.value;
    delete filtro.id;
    this.filtrosService.setFiltro(filtro).subscribe((result) => {
      alert('Filtro inserido com sucesso.');
    });
  }

  excluir(): void {
    const filtro: Filtro = this.formulario.value;
    this.filtrosService.excluir(filtro.id).subscribe((result) => {
      alert('Filtro excluído com sucesso.');
    });
  }

  alterar(): void {
    const filtro: Filtro = this.formulario.value;
    var id = filtro.id;
    delete filtro.id;
    this.filtrosService.alterar(id, filtro).subscribe((result) => {
      alert('Filtro alterado com sucesso.');
    });
  }
}
