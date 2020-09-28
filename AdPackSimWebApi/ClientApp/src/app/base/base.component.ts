import { HttpClient } from '@angular/common/http';
import { Data } from '../data';

export class BaseComponent {
  private type: string;
  private title: string;
  private originUrl: string;
  private http: HttpClient;
  private data: Data;

  constructor(type: string, title: string, originUrl: string, http: HttpClient) {
    this.type = type;
    this.title = title;
    this.originUrl = originUrl;
    this.http = http;
    this.data = new Data();
  }

  //TODO: Use a service
  private SendRequest(url: string, input: Data) {
    this.http.post<Data>(`${this.originUrl}/api/${this.type}/${url}`, input)
      .subscribe(data => this.data = data, error => console.error(error));
  }

  private OptimalTotalDays_Click() {
    this.SendRequest('OptimalTotalDays', this.data);
  }

  private OptimalReinvestingDays_Click() {
    this.SendRequest('OptimalReinvestingDays', this.data);
  }

  private OptimalInitialPacks_Click() {
    this.SendRequest('OptimalInitialPacks', this.data);
  }

  private Calculate_Click() {
    this.SendRequest('Calculate', this.data);
  }

  private ToROL_Click() {
    this.SendRequest('ToROL', this.data);
  }
}
