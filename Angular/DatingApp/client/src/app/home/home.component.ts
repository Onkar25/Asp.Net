import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient);
  users: any;
  registerMode = false;

  ngOnInit(): void {
    this.getUser();
  }


  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean)
  {
    this.registerMode = event;
  }
  getUser() {
    this.http.get("http://localhost:5000/api/users").subscribe(
      {
        next: response => this.users = response,
        error: error => console.log(error),
        complete: () => console.log('Request Completed')
      }
    );
  }
}
