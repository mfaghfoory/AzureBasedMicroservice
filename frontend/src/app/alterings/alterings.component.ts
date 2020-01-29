import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppMainServiceService } from '../services/app-main-service.service';
import { Alterations } from '../models/alterations';
import { Customers } from '../models/customers';

@Component({
  selector: 'app-alterings',
  templateUrl: './alterings.component.html',
  styleUrls: ['./alterings.component.sass']
})
export class AlteringsComponent implements OnInit {

  isLoading = false;
  constructor(private route: ActivatedRoute, private service: AppMainServiceService) { }
  data: Alterations[];
  customer: Customers;
  ngOnInit() {
    this.route.params
      .subscribe(p => {
        const customerId = p.id;
        this.loadCustomerInfo(customerId);
        this.loadData(customerId);
      });
  }

  loadData(customerId: number) {
    this.isLoading = true;
    this.service.getAlterationsByCustomerId(customerId).subscribe(x => {
      this.data = x;
      this.isLoading = false;
    }, err => {
      this.isLoading = false;
      alert(err.message);
    });
  }

  loadCustomerInfo(customerId: number) {
    this.service.getCustomerById(customerId).subscribe(x => {
      this.customer = x;
    })
  }
}
