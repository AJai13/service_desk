import { Usuario} from "./Usuario";

export class Dispositivo {
    id?: number = 0;
    nome: string = '';
    usuario: Usuario | undefined;
}