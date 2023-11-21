import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { StatusService } from 'src/app/status.service';
import { Status } from 'src/app/Status';


@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})
export class StatusComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  status: Array<Status> | undefined;

  constructor(private statusService: StatusService) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Novo status';
    this.statusService.listar().subscribe(status => {
      this.status = status;
    });
    this.formulario = new FormGroup({
      nome: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const status: Status = this.formulario.value;
    delete status.id;
    this.statusService.setStatus(status).subscribe((result) => {
      alert('Status inserido com sucesso.');
    });
  }

  excluir(): void {
    const status: Status = this.formulario.value;
    this.statusService.excluir(status.id).subscribe((result) => {
      alert('Status excluído com sucesso.');
    });
  }

  alterar(): void {
    const status: Status = this.formulario.value;
    var id = status.id;
    delete status.id;
    this.statusService.alterar(id, status).subscribe((result) => {
      alert('Status alterado com sucesso.');
    });
  }
}
