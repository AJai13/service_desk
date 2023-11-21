import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SolucoesService } from 'src/app/solucoes.service';
import { Solucao } from 'src/app/Solucao';
@Component({
  selector: 'app-solucoes',
  templateUrl: './solucoes.component.html',
  styleUrls: ['./solucoes.component.css']
})
export class SolucoesComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  solucoes: Array<Solucao> | undefined;

  constructor(private solucoesService: SolucoesService) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Nova solução';
    this.solucoesService.listar().subscribe(solucoes => {
      this.solucoes = solucoes;
    });
    this.formulario = new FormGroup({
      descSolucao: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const solucao: Solucao = this.formulario.value;
    delete solucao.id;
    this.solucoesService.setSolucao(solucao).subscribe((result) => {
      alert('Solução inserida com sucesso.');
    });
  }

  excluir(): void {
    const solucao: Solucao = this.formulario.value;
    this.solucoesService.excluir(solucao.id).subscribe((result) => {
      alert('Solução excluído com sucesso.');
    });
  }

  alterar(): void {
    const solucao: Solucao = this.formulario.value;
    var id = solucao.id;
    delete solucao.id;
    this.solucoesService.alterar(id, solucao).subscribe((result) => {
      alert('Solução alterado com sucesso.');
    });
  }
}
