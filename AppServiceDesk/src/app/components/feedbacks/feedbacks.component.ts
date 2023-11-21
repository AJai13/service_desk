import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { FeedbacksService } from 'src/app/feedbacks.service';
import { Feedback } from 'src/app/Feedback';
import { Solucao } from 'src/app/Solucao';
import { SolucoesService } from 'src/app/solucoes.service';


@Component({
  selector: 'app-feedbacks',
  templateUrl: './feedbacks.component.html',
  styleUrls: ['./feedbacks.component.css']
})
export class FeedbacksComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  solucoes: Array<Solucao> | undefined;
  feedbacks: Array<Feedback> | undefined;

  constructor(private feedbacksService: FeedbacksService, private solucoesService: SolucoesService) { }
  ngOnInit(): void {
    this.tituloFormulario = 'Novo feedback';
    this.feedbacksService.listar().subscribe(feedbacks => {
      console.log(feedbacks)
      this.feedbacks = feedbacks;
    });
    this.solucoesService.listar().subscribe(solucoes => {
      this.solucoes = solucoes;
      if (this.solucoes && this.solucoes.length > 0) {
        this.formulario.get('solucaoId')?.setValue(this.solucoes[0].id);
      }
    });
    this.formulario = new FormGroup({
      feedbackText: new FormControl(""),
      solucaoId: new FormControl(""),
      id: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const feedback: Feedback = this.formulario.value;
    delete feedback.id;
    this.feedbacksService.setFeedbackAssociarASolucao(feedback).subscribe((result) => {
      alert('Feedback inserido com sucesso.');
    });
  }

  excluir(): void {
    const feedback: Feedback = this.formulario.value;
    this.feedbacksService.excluir(feedback.id).subscribe((result) => {
      alert('Feedback excluído com sucesso.');
    });
  }

  alterar(): void {
    const feedback: Feedback = this.formulario.value;
    var id = feedback.id;
    delete feedback.id;
    this.feedbacksService.alterar(id, feedback).subscribe((result) => {
      alert('Feedback alterado com sucesso.');
    });
  }
}

