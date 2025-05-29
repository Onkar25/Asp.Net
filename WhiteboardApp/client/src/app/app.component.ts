import { Component } from '@angular/core';
import { CanvasBaordComponent } from "./boards/canvas-baord/canvas-baord.component";
import { HubComponent } from "./hub/hub.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CanvasBaordComponent, HubComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
}