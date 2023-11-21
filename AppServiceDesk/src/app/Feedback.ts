import { Solucao } from "./Solucao"

export class Feedback {
    id?: number = 0;
    feedbackText: string = "";
    solucao: Solucao | undefined;
    solucaoId: number = 0;
  }
