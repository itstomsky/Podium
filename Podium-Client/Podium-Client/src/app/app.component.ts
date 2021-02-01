import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Podium-Client';
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;



  constructor(private _formBuilder: FormBuilder) { }

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

  submit() {
    console.log(this.firstFormGroup.value);
    console.log(this.secondFormGroup.value);
  }
}
