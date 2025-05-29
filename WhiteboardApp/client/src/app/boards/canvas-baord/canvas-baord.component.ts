import { AfterViewInit, Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-canvas-baord',
  standalone: true,
  imports: [],
  templateUrl: './canvas-baord.component.html',
  styleUrl: './canvas-baord.component.css'
})
export class CanvasBaordComponent implements AfterViewInit {
  private _canvas!: HTMLCanvasElement;
  private context!: CanvasRenderingContext2D;
  private paint!: boolean;

  private clickX: number[] = [];
  private clickY: number[] = [];
  private clickDrag: boolean[] = [];

  @HostListener('document:mousedown', ['$event'])
  pressMouseEventHandler(event: MouseEvent) {
    console.log("mousedown");
    this.pressEventHandler(event);
  }

  @HostListener('document:touchstart', ['$event'])
  pressTouchEventHandler(event: TouchEvent) {
    console.log("touchstart");
    this.pressEventHandler(event);
  }

  @HostListener('document:mousemove', ['$event'])
  mouseDragEventHandler(event: MouseEvent) {
    console.log("mousemove");
    this.dragEventHandler(event);
  }

  @HostListener('document:touchmove', ['$event'])
  touchDragEventHandler(event: TouchEvent) {
    console.log("touchmove");
    this.dragEventHandler(event);
  }

  @HostListener('document:mouseup', ['$event'])
  mouseReleaseEventHandler(event: MouseEvent) {
    console.log("mouseup");
    this.releaseEventHandler();
  }

  @HostListener('document:touchend', ['$event'])
  touchReleaseEventHandler(event: TouchEvent) {
    console.log("touchend");
    this.releaseEventHandler();
  }

  @HostListener('document:mouseout', ['$event'])
  cancelMouseEventHandler() {
    console.log("mouseout");
    this.cancelEventHandler();
  }

  @HostListener('document:touchcancel', ['$event'])
  cancelTouchEventHandler() {
    console.log("touchcancel");
    this.cancelEventHandler();
  }

  ngAfterViewInit() {
    let _canvas = document.getElementById('canvas') as HTMLCanvasElement;
    let context = _canvas.getContext('2d');
    if (context != null) {
      context.lineCap = 'round';
      context.lineJoin = 'round';
      context.strokeStyle = 'black';
      context.lineWidth = 1;

      this._canvas = _canvas;
      this.context = context;

      this.redraw();
      this.createUserEvents();
    }
  }

  constructor() { }
  @ViewChild('clearBtn') clearBtnRef!: ElementRef<HTMLButtonElement>;
  private createUserEvents() {
    this.clearBtnRef.nativeElement.addEventListener('click', this.clearEventHandler);
  }

  private redraw() {
    let clickX = this.clickX;
    let context = this.context;
    let clickDrag = this.clickDrag;
    let clickY = this.clickY;
    for (let i = 0; i < clickX.length; ++i) {
      context.beginPath();
      if (clickDrag[i] && i) {
        context.moveTo(clickX[i - 1], clickY[i - 1]);
      } else {
        context.moveTo(clickX[i] - 1, clickY[i]);
      }

      context.lineTo(clickX[i], clickY[i]);
      context.stroke();
    }
    context.closePath();
  }

  private addClick(x: number, y: number, dragging: boolean) {
    this.clickX.push(x);
    this.clickY.push(y);
    this.clickDrag.push(dragging);
  }

  private clearCanvas() {
    this.context.clearRect(0, 0, this._canvas.width, this._canvas.height);
    this.clickX = [];
    this.clickY = [];
    this.clickDrag = [];
  }

  private clearEventHandler = () => {
    this.clearCanvas();
  };

  private releaseEventHandler = () => {
    this.paint = false;
    this.redraw();
  };

  private cancelEventHandler = () => {
    this.paint = false;
  };

  private pressEventHandler(e: MouseEvent | TouchEvent) {
    let mouseX = (e as TouchEvent).changedTouches
      ? (e as TouchEvent).changedTouches[0].pageX
      : (e as MouseEvent).pageX;
    let mouseY = (e as TouchEvent).changedTouches
      ? (e as TouchEvent).changedTouches[0].pageY
      : (e as MouseEvent).pageY;
    mouseX -= this._canvas.offsetLeft;
    mouseY -= this._canvas.offsetTop;

    this.paint = true;
    this.addClick(mouseX, mouseY, false);
    this.redraw();
  }

  private dragEventHandler(e: MouseEvent | TouchEvent) {
    let mouseX = (e as TouchEvent).changedTouches
      ? (e as TouchEvent).changedTouches[0].pageX
      : (e as MouseEvent).pageX;
    let mouseY = (e as TouchEvent).changedTouches
      ? (e as TouchEvent).changedTouches[0].pageY
      : (e as MouseEvent).pageY;
    mouseX -= this._canvas.offsetLeft;
    mouseY -= this._canvas.offsetTop;

    if (this.paint) {
      this.addClick(mouseX, mouseY, true);
      this.redraw();
    }

    e.preventDefault();
  }

  saveDrawing(): void {
    const data = {
      clickX: this.clickX,
      clickY: this.clickY,
      clickDrag: this.clickDrag,
    };
    localStorage.setItem('drawing', JSON.stringify(data));
    alert('Drawing saved!');
  }

  loadDrawing(): void {
    const data = localStorage.getItem('drawing');
    if (data) {
      const { clickX, clickY, clickDrag } = JSON.parse(data);
      this.clickX = clickX;
      this.clickY = clickY;
      this.clickDrag = clickDrag;
      this.redraw();
    } else {
      alert('No drawing found.');
    }
  }
}
