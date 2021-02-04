import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { AvailableProductsComponent } from './available-products/available-products.component';
import { PodiumService } from './services/podium.service';
import { ConfigService } from './services/config.service';
import { AppConfiguration } from './models/app-configuration';
import { HttpClient, HttpClientModule } from '@angular/common/http';

export function initializer(configService: ConfigService){
  return() => {
    return configService.load();
  }
}

@NgModule({
  declarations: [
    AppComponent,
    AvailableProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatStepperModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatListModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatToolbarModule,
    MatTableModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: AppConfiguration,
      deps: [HttpClient],
      useExisting: ConfigService
    },
    {
      provide: APP_INITIALIZER,
      multi: true,
      deps: [ConfigService],
      useFactory: initializer
    },
    MatDatepickerModule,
    PodiumService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
