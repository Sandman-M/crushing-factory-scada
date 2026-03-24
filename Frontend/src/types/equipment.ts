export type EquipmentStatus = 'OFF' | 'RUN' | 'IDLE' | 'WARNING' | 'ALARM';

export interface Equipment {
    id: string;
    name: string;
    type: string;
    status: EquipmentStatus;
    temperature?: number;
    load?: number;
    level?: number;
}