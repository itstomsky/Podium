import { Component, OnInit } from '@angular/core';
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
export class AppComponent implements OnInit{
  title: string;
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  registeredUserID: string;
  availableProducts: Product[];


  constructor(private formBuilder: FormBuilder, private config: AppConfiguration, private podiumService: PodiumService) {
    this.title = config.title;
    this.availableProducts = [];
  }

  ngOnInit(): void{
    this.firstFormGroup = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      dob: ['', Validators.required],
      email: ['', Validators.required]
    });

    this.secondFormGroup = this.formBuilder.group({
      propertyValue: ['', Validators.required],
      depositAmount: ['', Validators.required]
    });
  }

  registerUser(): void{
    if (this.firstFormGroup.valid){
      const user = new User();

      user.firstName = this.firstFormGroup.get('firstname').value;
      user.lastName = this.firstFormGroup.get('lastname').value;
      user.dateOfBirth = this.firstFormGroup.get('dob').value;
      user.email = this.firstFormGroup.get('email').value;

      this.podiumService.registerUser(user).subscribe(data => this.registeredUserID = data.toString());
    }
  }

  getAvailableProducts(): void{
    if (this.secondFormGroup.valid){
      const propertyValue = this.secondFormGroup.get('propertyValue').value;
      const depositAmount = this.secondFormGroup.get('depositAmount').value;

      this.podiumService.getAvailableProducts(this.registeredUserID, propertyValue, depositAmount).subscribe(data => {
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
