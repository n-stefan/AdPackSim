import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'genericlist',
  templateUrl: '../base/base.component.html',
  styleUrls: ['../base/base.component.css']
})
export class GenericListComponent extends BaseComponent {
  constructor(@Inject('ORIGIN_URL') originUrl: string, http: HttpClient) {
    super('GenericList', 'Safe code using a generic List', originUrl, http);
  }
}
