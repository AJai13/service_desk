import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Ticket } from 'src/app/Ticket';
import { TicketsService } from 'src/app/tickets.service';
import { UsuariosService } from 'src/app/usuarios.service';
import { Usuario } from 'src/app/Usuario';
import { Dispositivo } from 'src/app/Dispositivo';
import { DispositivosService } from 'src/app/dispositivos.service';
import { Status } from 'src/app/Status';
import { StatusService } from 'src/app/status.service';
import { Prioridade } from 'src/app/Prioridade';
import { PrioridadesService } from 'src/app/prioridades.service';
import { Categoria } from 'src/app/Categoria';
import { CategoriasService } from 'src/app/categorias.service';
import { Sla } from 'src/app/Sla';
import { SlasService } from 'src/app/slas.service';
import { Funcionario } from 'src/app/Funcionario';
import { FuncionariosService } from 'src/app/funcionarios.service';
import { Solucao } from 'src/app/Solucao';
import { SolucoesService } from 'src/app/solucoes.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css'],
})
export class TicketsComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = '';
  tickets: Array<Ticket> | undefined;
  dispositivos: Array<Dispositivo> | undefined;
  statuses: Array<Status> | undefined;
  prioridades: Array<Prioridade> | undefined;
  categorias: Array<Categoria> | undefined;
  slas: Array<Sla> | undefined;
  funcionarios: Array<Funcionario> | undefined;
  usuarios: Array<Usuario> | undefined;
  solucoes: Array<Solucao> | undefined;

  constructor(
    private ticketsService: TicketsService,
    private usuariosService: UsuariosService,
    private dispositivosService: DispositivosService,
    private statusService: StatusService,
    private prioridadesService: PrioridadesService,
    private categoriasService: CategoriasService,
    private slasService: SlasService,
    private funcionariosService: FuncionariosService,
    private solucoesService: SolucoesService,
  ) {}
  ngOnInit(): void {
    this.tituloFormulario = 'Novo ticket';
    this.ticketsService.listar().subscribe((tickets) => {
      this.tickets = tickets;
    });
    this.usuariosService.listar().subscribe((usuarios) => {
      this.usuarios = usuarios;
      if (this.usuarios && this.usuarios.length > 0) {
        this.formulario.get('usuarioId')?.setValue(this.usuarios[0].id);
      }
    });
    this.prioridadesService.listar().subscribe((prioridades) => {
      this.prioridades = prioridades;
      if (this.prioridades && this.prioridades.length > 0) {
        this.formulario.get('prioridadeId')?.setValue(this.prioridades[0].id);
      }
    });
    this.categoriasService.listar().subscribe((categorias) => {
      this.categorias = categorias;
      if (this.categorias && this.categorias.length > 0) {
        this.formulario.get('categoriaId')?.setValue(this.categorias[0].id);
      }
    });
    this.dispositivosService.listar().subscribe((dispositivos) => {
      this.dispositivos = dispositivos;
      if (this.dispositivos && this.dispositivos.length > 0) {
        this.formulario.get('dispositivoId')?.setValue(this.dispositivos[0].id);
      }
    });
    this.statusService.listar().subscribe((statuses) => {
      this.statuses = statuses;
      if (this.statuses && this.statuses.length > 0) {
        this.formulario.get('statusId')?.setValue(this.statuses[0].id);
      }
    });
    this.slasService.listar().subscribe((slas) => {
      this.slas = slas;
      if (this.slas && this.slas.length > 0) {
        this.formulario.get('slaId')?.setValue(this.slas[0].id);
      }
    });
    this.funcionariosService.listar().subscribe((funcionarios) => {
      this.funcionarios = funcionarios;
      if (this.funcionarios && this.funcionarios.length > 0) {
        this.formulario.get('funcionarioId')?.setValue(this.funcionarios[0].id);
      }
    });
    this.solucoesService.listar().subscribe((solucoes) => {
      this.solucoes = solucoes;
      if (this.solucoes && this.solucoes.length > 0) {
        this.formulario.get('solucaoId')?.setValue(this.solucoes[0].id);
      }
    });
    // resto das demais classes atreladas
    //...
    this.formulario = new FormGroup({
      id: new FormControl(""),
      titulo: new FormControl(""),
      descricao: new FormControl(""),
      dataAbertura: new FormControl(""),
      statusId: new FormControl(""),
      prioridadeId: new FormControl(""),
      categoriaId: new FormControl(""),
      dispositivoId: new FormControl(""),
      usuarioId: new FormControl(""),
      funcionarioId: new FormControl(""),
      solucaoId: new FormControl(""),
      slaId: new FormControl(""),
    });
  }
  enviarFormulario(): void {
    const ticket: Ticket = this.formulario.value;
    delete ticket.id;
    this.ticketsService.criarTicket(ticket).subscribe((result) => {
      alert('Ticket inserido com sucesso.');
    });
  }

  excluir(): void {
    const ticket: Ticket = this.formulario.value;
    this.ticketsService.excluir(ticket.id).subscribe((result) => {
      alert('Ticket excluído com sucesso.');
    });
  }

  alterar(): void {
    const ticket: Ticket = this.formulario.value;
    var id = ticket.id;
    delete ticket.id;
    this.ticketsService.alterar(id, ticket).subscribe((result) => {
      alert('Ticket alterado com sucesso.');
    });
  }
}
