import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GenericListComponent } from './genericlist/genericlist.component';
import { UnsafeStructArrayComponent } from './unsafestructarray/unsafestructarray.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GenericListComponent,
    UnsafeStructArrayComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'genericlist', component: GenericListComponent },
      { path: 'unsafestructarray', component: UnsafeStructArrayComponent },
    ])
  ],
  providers: [
    { provide: 'ORIGIN_URL', useValue: location.origin }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
