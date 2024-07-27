
export interface TaskRequestDto {
    id: number | null;
    subject: string;
    projectId: number;
    assignTo: number | null;
    priority: number;
    type: number;
    assignDate: Date | null;
    dueDate: Date | null;
    description: string | null;
    attachment: string;
    status: number | null;
    isActive: boolean;
}