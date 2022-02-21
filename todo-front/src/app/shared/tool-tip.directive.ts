import { AfterViewInit, Directive, ElementRef, Input } from '@angular/core';
import tippy from 'tippy.js';

@Directive({
  selector: '[appToolTip]',
})
export class ToolTipDirective implements AfterViewInit {
  @Input('appToolTip') toolTipContent!: string;

  constructor(private elRef: ElementRef) {}

  ngAfterViewInit(): void {
    tippy(this.elRef.nativeElement, {
      content: this.toolTipContent,
      animation: 'fade',
      delay: 100,
    });
  }
}
