export interface UserDto {
    id: number;
    fullName: string;
    email: string;
    password: string;
    phoneNumber: string | null;
    roleId: number;
    managerId: number;
    isActive: boolean;
}