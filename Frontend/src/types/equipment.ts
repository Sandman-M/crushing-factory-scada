export type EquipmentStatus = 'OFF' | 'RUN' | 'IDLE' | 'WARNING' | 'ALARM';

export interface Equipment {
    id: string;
    name: string;
    status: EquipmentStatus;
    temperature: number;
    load: number;
}