export interface TaskReportRequest {
    projectId: number | null;
    assignTo: number | null;
    type: TaskType | null;
    priority: number | null;
    assignFromDate: Date;
    assignToDate: Date;
    dueTask: boolean;
    status: number | null;
}