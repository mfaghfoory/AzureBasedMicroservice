import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppMainServiceService } from '../services/app-main-service.service';
import { Alterations } from '../models/alterations';
import { Customers } from '../models/customers';
import { NewPayment } from '../models/new-payment';

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
  currentAlteration = 0;
  amount = 0;
  creatingPayment = false;
  paymentError = '';
  @ViewChild('closePaymentModal', { static: true }) closePaymentModal: ElementRef;
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

  onCreateNewPayment() {
    this.paymentError = null;
    this.creatingPayment = true;
    this.service.registerNewPayment({ alteringId: this.currentAlteration, amount: this.amount }).
      subscribe(x => {
        this.creatingPayment = false;
        setTimeout(() => { this.closePaymentModal.nativeElement.click(); }, 50)
      }, err => {
        if (err.status == 400 && err.error) {
          let allErrors = new Array<string>();
          for (let e in err.error) {
            let errors: [] = err.error[e];
            errors && errors.forEach(xx => {
              allErrors.push(xx);
            })
          }
          this.paymentError = allErrors.join('\r\n');
          this.creatingPayment = false;
        }
      })
  }
}
