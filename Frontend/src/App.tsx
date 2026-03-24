import { useState } from 'react'
import { useEffect } from 'react';
import type { Equipment } from './types/equipment';
import MimicPanel from './components/MimicPanel';
import EquipmentDetails from './components/EquipmentDetails';

import './App.css'

function App() {
  const [equipmentData, setEquipmentData] = useState<Equipment[]>([]);
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [isAnimating, setIsAnimating] = useState<boolean>(false);

  const selectedEquipment = equipmentData.find(eq => eq.id === selectedId) || null;

  useEffect(() => {
    const fetchEquipment = async () => {
      try {
        const response = await fetch('https://localhost:7254/api/equipment');
        if (response.ok) {
          const data: Equipment[] = await response.json();
          setEquipmentData(data);
        }
      } catch (error) {
        console.error('Помилка підключення до API:', error);
      }
    };

    fetchEquipment();

    const intervalId = setInterval(fetchEquipment, 2000);

    return () => clearInterval(intervalId);
  }, []);

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>Мнемосхема дробарної фабрики</h1>
        <button 
          className={`btn-start ${isAnimating ? 'active' : ''}`}
          onClick={() => setIsAnimating(!isAnimating)}
        >
          {isAnimating ? 'Stop System' : 'Start System'}
        </button>
      </header>

      <main className="workspace">
        <div className="mimic-area">
          <MimicPanel
            equipmentData={equipmentData}
            onEquipmentClick={(id) => setSelectedId(id)}
            isAnimating={isAnimating}
            />
        </div>

        <div className="details-area">
          <EquipmentDetails equipment={selectedEquipment} />
        </div>
      </main>
    </div>
  );
}

export default App
