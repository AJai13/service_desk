import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PrioridadesService } from 'src/app/prioridades.service';
import { Prioridade } from 'src/app/Prioridade';
@Component({
  selector: 'app-prioridades',
  templateUrl: './prioridades.component.html',
  styleUrls: ['./prioridades.component.css']
})
export class PrioridadesComponent implements OnInit  {
  formulario: any;
  tituloFormulario: string = '';
  prioridades: Array<Prioridade> | undefined;

  constructor(private prioridadesService: PrioridadesService) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Nova prioridade';
    this.prioridadesService.listar().subscribe(prioridades => {
      this.prioridades = prioridades;
    });
    this.formulario = new FormGroup({
      nome: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const prioridade: Prioridade = this.formulario.value;
    delete prioridade.id;
    this.prioridadesService.setPrioridade(prioridade).subscribe((result) => {
      alert('Prioridade inserida com sucesso.');
    });
  }

  excluir(): void {
    const prioridade: Prioridade = this.formulario.value;
    this.prioridadesService.excluir(prioridade.id).subscribe((result) => {
      alert('Prioridade excluído com sucesso.');
    });
  }

  alterar(): void {
    const prioridade: Prioridade = this.formulario.value;
    var id = prioridade.id;
    delete prioridade.id;
    this.prioridadesService.alterar(id, prioridade).subscribe((result) => {
      alert('Prioridade alterado com sucesso.');
    });
  }
}
