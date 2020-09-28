import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'unsafestructarray',
  templateUrl: '../base/base.component.html',
  styleUrls: ['../base/base.component.css']
})
export class UnsafeStructArrayComponent extends BaseComponent {
  constructor(@Inject('ORIGIN_URL') originUrl: string, http: HttpClient) {
    super('UnsafeStructArray', 'Unsafe code using a struct Array', originUrl, http);
  }
}
