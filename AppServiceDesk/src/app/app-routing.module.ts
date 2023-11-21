import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriasComponent } from './components/categorias/categorias.component';
import { StatusComponent } from './components/status/status.component';
import { SolucoesComponent } from './components/solucoes/solucoes.component';
import { SlasComponent } from './components/slas/slas.component';
import { PrioridadesComponent } from './components/prioridades/prioridades.component';
import { FiltrosComponent } from './components/filtros/filtros.component';
import { FeedbacksComponent } from './components/feedbacks/feedbacks.component';
import { FuncionariosComponent } from './components/funcionarios/funcionarios.component';
import { DispositivosComponent } from './components/dispositivos/dispositivos.component';
import { UsuariosComponent } from './components/usuarios/usuarios.component';
import { CentroDeCustosComponent } from './components/centro-de-custos/centro-de-custos.component';
import { TicketsComponent } from './components/tickets/tickets.component';

const routes: Routes = [
  { path: 'categorias', component: CategoriasComponent },
  { path: 'status', component: StatusComponent },
  { path: 'solucoes', component: SolucoesComponent },
  { path: 'slas', component: SlasComponent },
  { path: 'prioridades', component: PrioridadesComponent },
  { path: 'filtros', component: FiltrosComponent },
  { path: 'feedbacks', component: FeedbacksComponent },
  { path: 'funcionarios', component: FuncionariosComponent },
  { path: 'dispositivos', component: DispositivosComponent },
  { path: 'usuarios', component: UsuariosComponent },
  { path: 'centroDeCustos', component: CentroDeCustosComponent },
  { path: 'tickets', component: TicketsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
