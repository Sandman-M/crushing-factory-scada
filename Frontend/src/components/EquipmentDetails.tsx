import React from "react";
import type { Equipment } from "../types/equipment";

interface EquipmentDetailsProps {
    equipment: Equipment | null;
}

const EquipmentDetails: React.FC<EquipmentDetailsProps> = ({ equipment }) => {
  if (!equipment) {
    return (
      <div style={{ textAlign: 'center', color: 'var(--text-muted)', marginTop: '50px' }}>
        <h3>Панель інформації</h3>
        <p>Клікніть на агрегат на мнемосхемі, щоб побачити його поточні показники.</p>
      </div>
    );
  }

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'RUN': return 'var(--status-run)';
      case 'ALARM': return 'var(--status-alarm)';
      case 'WARNING': return 'var(--status-warning)';
      case 'IDLE': return 'var(--status-idle)';
      default: return 'var(--status-off)';
    }
  };

  return (
    <div className="details-card">
      <h2 style={{ borderBottom: '1px solid #555', paddingBottom: '10px' }}>
        {equipment.name}
      </h2>
      
      <div style={{ display: 'flex', flexDirection: 'column', gap: '15px', marginTop: '20px', fontSize: '1.2rem' }}>
        
        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
          <span>Статус:</span>
          <strong style={{ color: getStatusColor(equipment.status) }}>
            {equipment.status}
          </strong>
        </div>

        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
          <span>Температура:</span>
          <strong>{equipment.temperature} °C</strong>
        </div>

        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
          <span>Навантаження:</span>
          <strong>{equipment.load} %</strong>
        </div>

      </div>
    </div>
  );
};

export default EquipmentDetails;