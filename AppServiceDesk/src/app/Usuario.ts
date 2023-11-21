import { Dispositivo } from "./Dispositivo";
import { CentroDeCusto} from "./CentroDeCusto";

export class Usuario {
    id?: number = 0;
    nome: string = "";
    email: string = "";
    centroDeCusto: CentroDeCusto | undefined;
    dispositivo: Dispositivo | undefined;
    centroDeCustoId: number = 0;
    dispositivoId: number = 0;
    usuario: Usuario | undefined;
  }
