import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, zip } from 'rxjs';
import { AppConfiguration } from '../models/app-configuration';
import { Product } from '../models/product.model'
import { User } from '../models/user.model'

@Injectable({
  providedIn: 'root'
})
export class PodiumService {
  
  private registerUserURI = '/User';
  private getAvailableProductsURI = '/Product/';

  constructor(private http: HttpClient,
              private config: AppConfiguration) { }

  registerUser(user: User): Observable<string>{
    let result : Observable<string>;

    result = this.http.post(`${this.config.apiURL}${this.registerUserURI}`, user, {responseType: 'text'});

    return result;
  }

  getAvailableProducts(userID: string, propertyValue: number, depositAmount: number): Observable<Product[]>{
    return this.http.get<Product[]>(`${this.config.apiURL}${this.getAvailableProductsURI}${userID}${'/'}${propertyValue}${'/'}${depositAmount}`);
  }
}
