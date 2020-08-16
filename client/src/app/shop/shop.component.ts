import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/type';
import { IBrand } from '../shared/models/brand';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  types: IType[];
  brands: IBrand[];
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected = 'name';
  sortOptions = [
    { name: 'Alphabetically', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getTypes();
    this.getBrands();
  }

  getProducts(): void {
    this.shopService
      .getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected)
      .subscribe(
        (response) => {
          this.products = response.data;
        },
        (error) => {
          console.log(error);
        }
      );
  }
  getTypes(): void {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getBrands(): void {
    this.shopService.getBrands().subscribe(
      (response) => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number): void {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string): void {
    this.sortSelected = sort;
    this.getProducts();
  }
}
