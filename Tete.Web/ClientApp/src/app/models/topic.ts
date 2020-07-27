export class Topic {
  public name: string;
  public description: string;
  public elligible: boolean;
  public mentorshipCount: number;

  constructor() {
    this.name = '';
    this.description = '';
    this.elligible = false;
    this.mentorshipCount = 0;
  }
}