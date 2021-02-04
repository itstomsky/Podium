import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PodiumService } from './services/podium.service';
import { User } from './models/user.model';
import { AppConfiguration } from './models/app-configuration';

import { Product } from './models/product.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title: string;
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  registeredUserID: string;
  availableProducts: Product[];


  constructor(private _formBuilder: FormBuilder, private _config: AppConfiguration, private _podiumService: PodiumService) {
    this.title = _config.title;
    this.availableProducts = [];
  }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      dob: ['', Validators.required],
      email: ['', Validators.required]
    });

    this.secondFormGroup = this._formBuilder.group({
      propertyValue: ['', Validators.required],
      depositAmount: ['', Validators.required]
    });
  }

  registerUser(){
    if (this.firstFormGroup.valid){
      let user = new User();

      user.firstName = this.firstFormGroup.get('firstname').value;
      user.lastName = this.firstFormGroup.get('lastname').value;
      user.dateOfBirth = this.firstFormGroup.get('dob').value;
      user.email = this.firstFormGroup.get('email').value;;

      this._podiumService.registerUser(user).subscribe(data => this.registeredUserID = data.toString());
    }
  }

  getAvailableProducts(){
    if(this.secondFormGroup.valid){
      let propertyValue = this.secondFormGroup.get('propertyValue').value;
      let depositAmount = this.secondFormGroup.get('depositAmount').value;

      this._podiumService.getAvailableProducts(this.registeredUserID, propertyValue, depositAmount).subscribe(data => {
        this.availableProducts = data;
      });
    }
  }

  ageFromDateOfBirthday(dateOfBirth: any): number {
    const today = new Date();
    const birthDate = new Date(dateOfBirth);
    let age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();

    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    return age;
  }
}
