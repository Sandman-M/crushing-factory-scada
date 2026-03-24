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
        <span style={{ fontSize: '0.8rem', color: '#999', marginLeft: '10px', fontWeight: 'normal' }}>
          ({equipment.type === 'Crusher' ? 'Дробарка' : equipment.type === 'Conveyor' ? 'Конвеєр' : 'Бункер'})
        </span>
      </h2>
      
      <div style={{ display: 'flex', flexDirection: 'column', gap: '15px', marginTop: '20px', fontSize: '1.2rem' }}>
        
        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
          <span>Статус:</span>
          <strong style={{ color: getStatusColor(equipment.status) }}>
            {equipment.status}
          </strong>
        </div>

        {equipment.temperature != null && (
          <div style={{ display: 'flex', justifyContent: 'space-between' }}>
            <span>Температура:</span>
            <strong>{equipment.temperature} °C</strong>
          </div>
        )}

        {equipment.load != null && (
          <div style={{ display: 'flex', justifyContent: 'space-between' }}>
            <span>Навантаження:</span>
            <strong>{equipment.load} %</strong>
          </div>
        )}

        {equipment.level != null && (
          <div style={{ display: 'flex', flexDirection: 'column', gap: '8px', marginTop: '10px' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <span>Рівень заповнення:</span>
              <strong>{equipment.level} %</strong>
            </div>
            <div style={{ width: '100%', height: '12px', backgroundColor: '#333', borderRadius: '6px', overflow: 'hidden' }}>
              <div style={{ 
                height: '100%', 
                width: `${equipment.level}%`, 
                backgroundColor: equipment.level >= 95 ? 'var(--status-alarm)' : equipment.level >= 85 ? 'var(--status-warning)' : 'var(--status-run)',
                transition: 'width 0.5s ease-in-out, background-color 0.5s ease'
              }} />
            </div>
          </div>
        )}

      </div>
    </div>
  );
};

export default EquipmentDetails;