import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../models/product.model';

@Component({
  selector: 'available-products',
  templateUrl: './available-products.component.html',
  styleUrls: ['./available-products.component.scss']
})
export class AvailableProductsComponent implements OnInit {
  @Input() products: Product[];
  displayedColumns: string[] = ['lender', 'interestRate', 'type'];

  constructor() {}

  ngOnInit(): void {
  }

}
