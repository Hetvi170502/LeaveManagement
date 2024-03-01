import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { leave } from 'src/app/models/leave.model';
import { EmployeService } from 'src/app/services/employe.service';

@Component({
  selector: 'app-leave-add',
  templateUrl: './leave-add.component.html',
  styleUrls: ['./leave-add.component.css']
})
export class LeaveAddComponent implements OnInit
{
  applyLeaveForm! : FormGroup;

  get startDate() {return this.applyLeaveForm.get('startDate')}
  get endDate() {return this.applyLeaveForm.get('endDate')}
  get reasonForLeave() {return this.applyLeaveForm.get('reasonForLeave')}
  get leaveTypeId() {return this.applyLeaveForm.get('leaveTypeId')}


  constructor(private employe : EmployeService , private formBuilder : FormBuilder , private router : Router ,
    public dialog: MatDialog ,  public dialogRef: MatDialogRef<LeaveAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: leave )
  {
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm():void
  {
    this.applyLeaveForm = new FormGroup({
      id : new FormControl(this.data.id ?? null,Validators.required),
      leaveTypeId : new FormControl(this.data.leaveTypeId,Validators.required),
      startDate : new FormControl(this.data.startDate,Validators.required),
      endDate : new FormControl(this.data.endDate,Validators.required),
      reasonForLeave : new FormControl(this.data.reasonForLeave,Validators.required),
    })
  }

  applyLeave(){
    console.log(this.applyLeaveForm.value);
    this.dialogRef.close(this.applyLeaveForm.value);
  }
  public close(){
    this.dialogRef.close();
  }

  minDate(): string {
    return new Date().toISOString().split('T')[0];
  }

}
