import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css'],
})
export class ShowDepComponent implements OnInit {
  constructor(private service: SharedService) {}

  DepartmentList: any = [];

  ModalTitle: string;
  ActivateAddEditDepComp: boolean = false;
  department: any;

  ngOnInit(): void {
    // this is the first method that gets executed when this component is rendered
    this.refreshDepList();
  }

  addClick() {
    this.department = {
      DepartmentId: 0,
      DepartmentName: '',
    };
    this.ModalTitle = 'Add Department';
    this.ActivateAddEditDepComp = true;
  }

  editClick(item) {
    this.department = item;
    this.ModalTitle = 'Edit Department';
    this.ActivateAddEditDepComp = true;
  }

  deleteClick(item) {
    if (confirm('Are you sure??')) {
      this.service.deleteDepartment(item.DepartmentId).subscribe((data) => {
        alert(data.toString());
        this.refreshDepList();
      });
    }
  }

  closeClick() {
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }

  refreshDepList() {
    this.service.getDepList().subscribe((data) => {
      this.DepartmentList = data;
    });
    // subscribe wait until the response is received, and only then assign value to the variable
    // remember that getDepList return a Observable
  }
}
