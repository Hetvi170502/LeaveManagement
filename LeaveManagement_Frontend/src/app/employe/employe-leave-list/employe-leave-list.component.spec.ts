import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeLeaveListComponent } from './employe-leave-list.component';

describe('EmployeLeaveListComponent', () => {
  let component: EmployeLeaveListComponent;
  let fixture: ComponentFixture<EmployeLeaveListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeLeaveListComponent]
    });
    fixture = TestBed.createComponent(EmployeLeaveListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
