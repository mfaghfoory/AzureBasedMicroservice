import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppMainServiceService } from '../services/app-main-service.service';
import { Alterations } from '../models/alterations';
import { Customers } from '../models/customers';
import { NewPayment } from '../models/new-payment';
import { NewAlteration } from '../models/new-alteration';

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
  pending = false;
  errorReturned = '';
  newAlteration: NewAlteration = null;
  @ViewChild('closePaymentModal', { static: true }) closePaymentModal: ElementRef;
  @ViewChild('closeAlterationModal', { static: true }) closeAlterationModal: ElementRef;
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
    this.errorReturned = null;
    this.pending = true;
    this.service.registerNewPayment({ alteringId: this.currentAlteration, amount: this.amount }).
      subscribe(x => {
        this.pending = false;
        setTimeout(() => { this.closePaymentModal.nativeElement.click(); }, 50)
      }, err => {
        if (err.status == 400 && err.error) {
          this.errorReturned = this.flattenError(err);
        }
        this.pending = false;
      })
  }

  onCreateNewAlteration() {
    this.errorReturned = null;
    this.pending = true;
    this.service.createAlteration(this.newAlteration).
      subscribe(x => {
        this.pending = false;
        setTimeout(() => { this.closeAlterationModal.nativeElement.click(); }, 50)
      }, err => {
        if (err.status == 400 && err.error) {
          this.errorReturned = this.flattenError(err);
        }
        this.pending = false;
      })
  }

  flattenError(err) {
    let allErrors = new Array<string>();
    for (let e in err.error) {
      let errors: [] = err.error[e];
      errors && errors.forEach(xx => {
        allErrors.push(xx);
      })
    }
    return allErrors.join('\r\n');
  }
}
