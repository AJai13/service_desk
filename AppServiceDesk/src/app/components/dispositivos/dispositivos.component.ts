import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DispositivosService } from 'src/app/dispositivos.service';
import { Dispositivo } from 'src/app/Dispositivo';

@Component({
  selector: 'app-dispositivos',
  templateUrl: './dispositivos.component.html',
  styleUrls: ['./dispositivos.component.css'],
})
export class DispositivosComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  dispositivos: Array<Dispositivo> | undefined;

  constructor(
    private dispositivosService: DispositivosService,
  ) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Novo dispositivo';
    this.dispositivosService.listar().subscribe((dispositivos) => {
      this.dispositivos = dispositivos;
    });
    this.formulario = new FormGroup({
      nome: new FormControl(''),
      id: new FormControl('')
    });
  }
  enviarFormulario(): void {
    const dispositivo: Dispositivo = this.formulario.value;
    delete dispositivo.id;
    this.dispositivosService.cadastrar(dispositivo).subscribe((result) => {
      alert('Dispositivo inserido com sucesso.');
    });
  }

  excluir(): void {
    const dispositivo: Dispositivo = this.formulario.value;
    this.dispositivosService.excluir(dispositivo.id).subscribe((result) => {
      alert('Dispositivo excluído com sucesso.');
    });
  }

  alterar(): void {
    const dispositivo: Dispositivo = this.formulario.value;
    var id = dispositivo.id;
    delete dispositivo.id;
    this.dispositivosService.alterar(id, dispositivo).subscribe((result) => {
      alert('Dispositivo alterado com sucesso.');
    });
  }
}
