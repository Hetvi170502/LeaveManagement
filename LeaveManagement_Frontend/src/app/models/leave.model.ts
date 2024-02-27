import { leaveType } from "./leaveType.model"
import { register } from "./register.model"

export interface leave
{
  id : number,
  userId : string,
  leaveTypeId : string,
  startDate : Date,
  endDate : Date,
  dateOfRequest : Date,
  reasonForLeave : string,
  status : string
  user:register,
  leaveType : leaveType
}


