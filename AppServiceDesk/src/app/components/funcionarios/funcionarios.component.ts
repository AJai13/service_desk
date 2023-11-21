import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { FuncionariosService } from 'src/app/funcionarios.service';
import { Funcionario } from 'src/app/Funcionario';

@Component({
  selector: 'app-funcionarios',
  templateUrl: './funcionarios.component.html',
  styleUrls: ['./funcionarios.component.css'],
})
export class FuncionariosComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  funcionarios: Array<Funcionario> | undefined;

  constructor(private funcionariosService: FuncionariosService) { }
  ngOnInit(): void {
    this.tituloFormulario = 'Novo funcionario';
    this.funcionariosService.listar().subscribe((funcionarios) => {
      this.funcionarios = funcionarios;
    });
    this.formulario = new FormGroup({
      id: new FormControl(""),
      nome: new FormControl(""),
      email: new FormControl(""),
      senha: new FormControl(""),
      cargo: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const funcionario: Funcionario = this.formulario.value;
    delete funcionario.id;
    this.funcionariosService.cadastrar(funcionario).subscribe((result) => {
      alert('Funcionario inserido com sucesso.');
    });
  }

  excluir(): void {
    const funcionario: Funcionario = this.formulario.value;
    this.funcionariosService.excluir(funcionario.id).subscribe((result) => {
      alert('Funcionario excluído com sucesso.');
    });
  }

  alterar(): void {
    const funcionario: Funcionario = this.formulario.value;
    var id = funcionario.id;
    delete funcionario.id;
    this.funcionariosService.alterar(id, funcionario).subscribe((result) => {
      alert('Funcionario alterado com sucesso.');
    });
  }
}
