import React from "react";
import type { Equipment } from "../types/equipment";
import { FactorySvg } from "./FactorySvg";
import '../App.css';
//import .MimicPanel.css;

interface MimicPanelProps {
    equipmentData: Equipment[];
    onEquipmentClick: (id:string) => void;
    isAnimating: boolean;
}

const MimicPanel: React.FC<MimicPanelProps> = ({ equipmentData, onEquipmentClick, isAnimating }) => {
  
  const getEquipmentClass = (id: string) => {
    const item = equipmentData.find(eq => eq.id === id);
    if (!item) return 'status-off';
    
    const animationClass = (isAnimating && item.status === 'RUN') ? 'is-animating' : '';
    
    return `equipment-node status-${item.status.toLowerCase()} ${animationClass}`;
  };

  return (
    <div className="svg-container" style={{ width: '100%', height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center', overflow: 'auto' }}>
      <FactorySvg 
        getClassForId={getEquipmentClass} 
        onItemClick={onEquipmentClick} 
        style={{ maxWidth: '100%', maxHeight: '100%' }}
      />
    </div>
  );
};

export default MimicPanel;