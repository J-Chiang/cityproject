import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  dtOptions: DataTables.Settings = {};

  ngOnInit(): void {
    this.dtOptions = {
      serverSide: true,
      processing: true,
      ajax: { url: '/api/values/Data' },
      columns: [
        {
          title: 'ID',
          data: 'id'
        },
        {
          title: 'DATE',
          data: 'date'
        },
        {
          title: 'IP',
          data: 'ip'
        },
        {
          title: 'CHIFFRE',
          data: 'chiffre'
        },
        {
          title: 'TEXT',
          data: 'text'
        }
      ]
    };
  }
}
