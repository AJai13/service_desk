import { Dispositivo } from "./Dispositivo";
import { Status } from "./Status";
import { Prioridade } from "./Prioridade";
import { Categoria } from "./Categoria";
import { Sla } from "./Sla";
import { Funcionario } from "./Funcionario";
import { Usuario } from "./Usuario";


export class Ticket {
    id?: number = 0;
    titulo: string = "";
    descricao: string = "";
    dataAbertura: Date = new Date();
    status: Status | undefined;
    statusId:  number = 0;
    prioridade: Prioridade | undefined;
    prioridadeId: number = 0;
    categoria: Categoria | undefined;
    categoriaId: number = 0;
    sla: Sla | undefined;
    slaId: number = 0;
    dispositivo: Dispositivo | undefined;
    dispositivoId: number = 0;
    usuario: Usuario | undefined;
    usuarioId: number = 0;
    funcionario: Funcionario | undefined;
    funcionarioId: number = 0;
  }
