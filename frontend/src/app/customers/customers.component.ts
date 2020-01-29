import { Component, OnInit } from '@angular/core';
import { AppMainServiceService } from '../services/app-main-service.service';
import { Customers } from '../models/customers';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.sass']
})
export class CustomersComponent implements OnInit {

  constructor(private service: AppMainServiceService) { }
  data: Customers[];
  ngOnInit() {
    this.loadCustomers();
  }

  loadCustomers() {
    this.service.getCustomers().subscribe(x => {
      this.data = x;
    });
  }
}
