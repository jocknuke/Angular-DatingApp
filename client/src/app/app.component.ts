import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {


  http = inject(HttpClient);
  title = 'my apJonatahsp';
  users:any;

    ngOnInit(): void {
   this.http.get('http://localhost:5085/api/users').subscribe({

    next: response => this.users=response,
    error: error => console.error(error),
    complete: () => console.log('Request completed')

   })
  
    }}
