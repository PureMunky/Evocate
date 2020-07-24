export class UserLanguage {
  public languageId: string;
  public name: string;
  public read: boolean;
  public speak: boolean;

  constructor() {
    this.languageId = '';
    this.name = '';
    this.read = false;
    this.speak = false;
  }
}