import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { IPaginationModel } from '../../Models/i-pagination-model';
import { IDriver } from '../../Models/idriver';
import { DriverService } from '../../Services/driver.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  drivers: IDriver[] = [];
  totalCount: number = 0;
  currentPage: number = 1
  paging: IPaginationModel = { pageNmuber:1,pageSize:10 };
  constructor(private driverService: DriverService) { }

  ngOnInit(): void {

    this.getData();

  }


  pageChanged(event: PageChangedEvent): void {
    this.paging.pageNmuber = event.page;
    this.getData();
  }

  getData() {
    this.driverService.getDriver(2).subscribe(result => {
      console.log(result['data']);
     this.drivers = [result['data']];
    }, error => console.error(error));
  }
}
