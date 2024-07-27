import { TaskNoteDto } from "./NotesDto";

export interface TaskDto {
    id: number;
    subject: string;
    projectId: number;
    assignTo: number;
    priority: number;
    type: number;
    assignDate: Date | null;
    dueDate: Date | null;
    description: string;
    attachment: string;
    status: string;
    isActive: boolean;
    projectName: string | null;
    assignUserName: string | null;
    notes: TaskNoteDto[];
}