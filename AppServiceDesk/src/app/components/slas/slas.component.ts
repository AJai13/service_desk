import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SlasService } from 'src/app/slas.service';
import { Sla } from 'src/app/Sla';

@Component({
  selector: 'app-slas',
  templateUrl: './slas.component.html',
  styleUrls: ['./slas.component.css']
})
export class SlasComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  slas: Array<Sla> | undefined;

  constructor(private slasService: SlasService) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Novo sla';
    this.slasService.listar().subscribe(slas => {
      this.slas = slas;
    });
    this.formulario = new FormGroup({
      nome: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const sla: Sla = this.formulario.value;
    delete sla.id;
    this.slasService.setSla(sla).subscribe((result) => {
      alert('Sla inserido com sucesso.');
    });
  }

  excluir(): void {
    const sla: Sla = this.formulario.value;
    this.slasService.excluir(sla.id).subscribe((result) => {
      alert('Sla excluído com sucesso.');
    });
  }

  alterar(): void {
    const sla: Sla = this.formulario.value;
    var id = sla.id;
    delete sla.id;
    this.slasService.alterar(id, sla).subscribe((result) => {
      alert('Sla alterado com sucesso.');
    });
  }
}
