
export interface TaskNoteDto {
    id: number;
    taskId: number;
    note: string;
    attachment: string | null;
    createdBy: string | null;
    createdTime: Date | null;
}