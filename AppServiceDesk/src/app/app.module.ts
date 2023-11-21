import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CategoriasService } from './categorias.service';
import { CategoriasComponent } from './components/categorias/categorias.component';
import { StatusComponent } from './components/status/status.component';
import { StatusService } from './status.service';
import { SolucoesComponent } from './components/solucoes/solucoes.component';
import { SolucoesService } from './solucoes.service';
import { SlasComponent } from './components/slas/slas.component';
import { SlasService } from './slas.service';
import { PrioridadesComponent } from './components/prioridades/prioridades.component';
import { PrioridadesService } from './prioridades.service';
import { FiltrosComponent } from './components/filtros/filtros.component';
import { FiltrosService } from './filtros.service';
import { FeedbacksComponent } from './components/feedbacks/feedbacks.component';
import { FeedbacksService } from './feedbacks.service';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { FuncionariosService } from './funcionarios.service';
import { DispositivosComponent } from './components/dispositivos/dispositivos.component';
import { DispositivosService } from './dispositivos.service';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { UsuariosService } from './usuarios.service';
import { CentroDeCustosComponent } from './components/centro-de-custos/centro-de-custos.component';
import { CentroDeCustosService } from './centro-de-custos.service';
import { TicketsComponent } from './components/tickets/tickets.component';
import { TicketsService } from './tickets.service';

@NgModule({
  declarations: [
    AppComponent,
    CategoriasComponent,
    StatusComponent,
    SolucoesComponent,
    SlasComponent,
    PrioridadesComponent,
    FiltrosComponent,
    FeedbacksComponent,
    FuncionariosComponent,
    DispositivosComponent,
    UsuariosComponent,
    CentroDeCustosComponent,
    TicketsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
  ],
  providers: [
    HttpClientModule,
    CategoriasService,
    StatusService,
    SolucoesService,
    SlasService,
    PrioridadesService,
    FiltrosService,
    FeedbacksService,
    FuncionariosService,
    DispositivosService,
    UsuariosService,
    CentroDeCustosService,
    TicketsService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
