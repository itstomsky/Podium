import { Component, OnInit } from '@angular/core';

export interface Product {
  lender: string;
  interest_rate: number;
  type: string;
}

const ELEMENT_DATA: Product[] = [
  { lender: "Bank 1", interest_rate: 2, type: "Fixed" },
  { lender: "Bank 2", interest_rate: 3, type: "Variable" },
  { lender: "Bank 3", interest_rate: 4, type: "Fixed" },
];

@Component({
  selector: 'available-products',
  templateUrl: './available-products.component.html',
  styleUrls: ['./available-products.component.scss']
})
export class AvailableProductsComponent implements OnInit {

  displayedColumns: string[] = ['lender', 'interest_rate', 'type'];
  dataSource = ELEMENT_DATA;

  constructor() {}

  ngOnInit(): void {
  }

}
