import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppConfiguration } from '../models/app-configuration';

@Injectable({
  providedIn: 'root'
})
export class ConfigService extends AppConfiguration{
  title: string;
  apiURL: string;

  constructor(private http: HttpClient) {
    super();
  }

  // This function needs to return a promise
  public async load(){
    try{
      const config = await this.http.get<AppConfiguration>('assets/config.json').toPromise();
      this.title = config.title;
      this.apiURL = config.apiURL;
    }catch(e){
      console.error('Could not load app configuration!');
    }
  }
}
